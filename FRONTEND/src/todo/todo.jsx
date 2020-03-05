import React, { Component } from "react";
import Axios from "axios";

import PageHeader from "../template/pageHeader";
import TodoForm from "./todoForm";
import TodoList from "./todoList";
import Pagination from '../helper/pagination';

const URL = "http://localhost:5000/api/v1/todos";

class Todo extends Component {
  constructor(props) {
    super(props);
    //this.getData();
    //this.getTotalRecords();
  }

  state = { 
    title: "", 
    data: [],
    pageSize: 1, // qtd de registros por pagina
    currentPage: 1 // pagina inicial sempre começara com valor 1
  }
  
  componentDidMount = () => {
    this.getData()
  }

  montarUrlApiGet = (title) => {
    const search = title ? `titleFilter=${title}&` : "";
    const {currentPage, pageSize} = this.state
    return (`${URL}/paginatedResult/${currentPage}/${pageSize}?${search}orderByFilter=Created&TipoOrderBy=Descending`);
  }

  getData = (title = "") => {
    const urlFiltro = this.montarUrlApiGet(title);

    Axios.get(urlFiltro).then(resp => {
      this.setState({
        ...this.state,
        title,
        data: resp.data.data.PaginatedResult
      });
    });
  }


  handleAdd = () => {
    const title = this.state.title;
    if(title.trim() != "")
      Axios.post(URL + "/add", { title }).then(resp => this.getData());

      this.getData(this.state.title);
  };

  handleSearch = filter => {
    this.getData(this.state.title);
  };

  handleUpdateStatus = todo => {
    Axios.put(`${URL}/updateStatus/${todo.Id}`).then(resp =>
      this.getData(this.state.title)
    );
  };

  

  getTotalRecords = () => {
    let total = 0

    Axios.get(URL).then(resp => {
      this.total = resp.data.data.length;
      //console.log('1->' + this.total);
    });

    //console.log('2->' + this.total);
    return this.total;
  }

  handleRemove = todo => {
    Axios.delete(`${URL}/delete/${todo.Id}`).then(resp =>
      this.getData(this.state.title)
    );
  };

  handleChange = e => {
    this.setState({ ...this.state, title: e.target.value });
  };

  handleClear = () => {
    this.getData();
  };

  handlePageChange = page => {
    console.log(page);
    this.setState({ ...this.state, currentPage: page });
    this.getData()
  };

  render() {
    return (
      <div>
        <PageHeader name="Tarefas" small="Cadastro"></PageHeader>
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
          itemsCount={this.getTotalRecords()}
          pageSize={this.state.pageSize}
          currentPage={this.state.currentPage}
          onPageChange={this.handlePageChange}
        />

      </div>
    );
  }
}

export default Todo;
