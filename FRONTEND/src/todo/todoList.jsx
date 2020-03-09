import React from "react";
import IconButton from "../template/iconbutton";
import If from "../helper/if";

export default props => {
  const renderRows = () => {
    const list = props.list || [];

    const pStyle = {
      textDecoration: "line-through"
    };

    if(list.length == 0) {
      return (
        <tr><td className="text-center" colSpan="2">Nenhum registro encontrado</td></tr>
      )
    }
      
    return list.map(todo => (
      <tr key={todo.Id}>
        <td className={todo.IsDone ? "table-warning" : ""}>{todo.Title}</td>
        <td
          className={todo.IsDone ? "table-warning text-center" : "text-center"}
        >
          <IconButton
            style="success"
            hide={todo.IsDone}
            icon="check"
            onClick={() => props.handleUpdateStatus(todo)}
          ></IconButton>
          <IconButton
            style="warning"
            hide={!todo.IsDone}
            icon="undo"
            onClick={() => props.handleUpdateStatus(todo)}
          ></IconButton>
          <IconButton
            style="danger"
            icon="trash-o"
            onClick={() => props.handleRemove(todo)}
          ></IconButton>
        </td>
      </tr>
    ));
  };

  return (
    <table className="table mt-3 table-hover table-sm table-bordered">
      <thead className="thead-light">
        <tr>
          <th>Descrição</th>
          <th className="tableActions text-center">Ações</th>
        </tr>
      </thead>
      <tbody>{renderRows()}</tbody>
    </table>
  );
};
