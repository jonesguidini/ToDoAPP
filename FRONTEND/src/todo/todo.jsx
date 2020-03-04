import React, { Component } from 'react';
import PageHeader from '../template/pageHeader'
import TodoForm from './todoForm'
import TodoList from './todoList'

class Todo extends Component {

    // constructor(props) {
    //     super(props)
    //     this.handleAdd = this.handleAdd.bind(this)
    // }

    handleAdd = () => {
        console.log('123');
    }

    render() { 
        return ( 
            <div>
                <PageHeader name='Tarefas' small='Cadastro'></PageHeader>
                <TodoForm handleAdd={this.handleAdd}/>
                <TodoList />
            </div>
         );
    }
}
 
export default Todo;