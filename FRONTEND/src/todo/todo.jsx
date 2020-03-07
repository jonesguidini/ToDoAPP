import React, { Component } from "react";
import Axios from "axios";

import PageHeader from "../template/pageHeader";
import TodoForm from "./todoForm";
import TodoList from "./todoList";
import Pagination from "../helper/pagination";
import TableInfo from "./tableInfo";

const URL = "http://localhost:5000/api/v1/todos";

class Todo extends Component {
  constructor(props) {
    super(props);
  }

  state = {
    title: "",
    data: [],
    pageSize: 10, // qtd de registros por pagina
    currentPage: 1, // pagina inicial sempre começara com valor 1
    totalPages: 0,
    totalData: 0
  };

  componentDidMount = () => {
    this.getData();
  };

  montarUrlApiGet = title => {
    const search = title ? `titleFilter=${title}&` : "";
    const { currentPage, pageSize } = this.state;
    return `${URL}/paginatedResult/${currentPage}/${pageSize}?${search}orderByFilter=Title&TipoOrderBy=Ascending`;
  };

  getData = (title = "") => {
    const urlFiltro = this.montarUrlApiGet(title);

    Axios.get(urlFiltro).then(resp => {
      this.setState({
        ...this.state,
        title,
        data: resp.data.data.PaginatedResult,
        totalData: resp.data.data.TotalData,
        totalPages: resp.data.data.TotalPages
      });
    });
  };

  handlePageChange = page => {
    this.setState({ ...this.state, currentPage: page }, () => this.getData());
  };

  handleAdd = () => {
    const title = this.state.title;
    if (title.trim() != "")
      Axios.post(URL + "/add", { title }).then(resp => {
        this.setState({ currentPage: 1 }, () => this.getData());
      });
  };

  handleSearch = filter => {
    this.getData(this.state.title);
  };

  handleUpdateStatus = todo => {
    Axios.put(`${URL}/updateStatus/${todo.Id}`).then(resp =>
      this.getData(this.state.title)
    );
  };

  handleRemove = todo => {
    Axios.delete(`${URL}/delete/${todo.Id}`).then(resp =>
      this.getData(this.state.title)
    );
  };

  componentDidUpdate = (prevProps, prevState) => {
    if (prevState.totalPages > this.state.totalPages) {
      const _currentPage =
        this.state.currentPage > this.state.totalPages
          ? this.state.totalPages
          : this.state.currentPage;
      this.setState({ currentPage: _currentPage }, () =>
        this.getData(this.state.title)
      );
    }
  };

  handleChange = e => {
    this.setState({ ...this.state, title: e.target.value });
  };

  handleClear = () => {
    this.setState(
      {
        title: "",
        data: [],
        pageSize: 3, // qtd de registros por pagina
        currentPage: 1, // pagina inicial sempre começara com valor 1
        totalPages: 0,
        totalData: 0
      },
      () => this.getData()
    );
  };

  render() {
    return (
      <div>
        <PageHeader
          name="Tarefas"
          small="Cadastro"
          qtdRegistros={this.state.totalData}
        ></PageHeader>
        <TodoForm
          title={this.state.title}
          handleChange={this.handleChange}
          handleAdd={this.handleAdd}
          handleSearch={this.handleSearch}
          handleClear={this.handleClear}
        />
        <TodoList
          list={this.state.data}
          handleRemove={this.handleRemove}
          handleUpdateStatus={this.handleUpdateStatus}
        />

        <Pagination
          totalPages={this.state.totalPages}
          currentPage={this.state.currentPage}
          onPageChange={this.handlePageChange}
        />

        {/* <TableInfo totalData={this.state.totalData} /> */}
      </div>
    );
  }
}

export default Todo;
