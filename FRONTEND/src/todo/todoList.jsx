import React from "react";
import IconButton from "../template/iconbutton";
import If from "../helper/if";

export default props => {
  const renderRows = () => {
    const list = props.list || [];

    const pStyle = {
      textDecoration: "line-through"
    };

    return list.map(todo => (
      <tr key={todo.Id}>
        <td className={todo.IsDone ? "markAsDone" : ""}>{todo.Title}</td>
        <td>
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
    <table className="table mt-3">
      <thead>
        <tr>
          <th>Descrição</th>
          <th className="tableActions">Ações</th>
        </tr>
      </thead>
      <tbody>{renderRows()}</tbody>
    </table>
  );
};
