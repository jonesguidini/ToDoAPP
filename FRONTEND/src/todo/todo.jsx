import React, { Component } from "react";
import Axios from "axios";

import PageHeader from "../template/pageHeader";
import TodoForm from "./todoForm";
import TodoList from "./todoList";

const URL = "http://localhost:5000/api/v1/todos";

class Todo extends Component {
  constructor(props) {
    super(props);
    this.refresh();
  }

  state = { title: "", list: [] };

  handleAdd = () => {
    const title = this.state.title;
    Axios.post(URL + "/add", { title }).then(resp => this.refresh());
  };

  handleSearch = filter => {
    this.refresh(this.state.title);
  };

  handleUpdateStatus = todo => {
    Axios.put(`${URL}/updateStatus/${todo.Id}`).then(resp =>
      this.refresh(this.state.title)
    );
  };

  refresh = (title = "", currentPage = 1, totalPerPage = 100) => {
    const search = title ? `titleFilter=${title}&` : "";
    const urlFiltro = `${URL}/paginatedResult/${currentPage}/${totalPerPage}?${search}orderByFilter=Created&TipoOrderBy=Descending`;

    Axios.get(urlFiltro).then(resp => {
      this.setState({
        ...this.state,
        title,
        list: resp.data.data.PaginatedResult
      });
    });
  };

  handleRemove = todo => {
    Axios.delete(`${URL}/delete/${todo.Id}`).then(resp =>
      this.refresh(this.state.title)
    );
  };

  handleChange = e => {
    this.setState({ ...this.state, title: e.target.value });
  };

  handleClear = () => {
    this.refresh();
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
          list={this.state.list}
          handleRemove={this.handleRemove}
          handleUpdateStatus={this.handleUpdateStatus}
        />
      </div>
    );
  }
}

export default Todo;
