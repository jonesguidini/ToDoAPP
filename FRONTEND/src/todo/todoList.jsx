import React from "react";
import IconButton from "../template/iconbutton";
import If from "../helper/if";

export default props => {
  const renderRows = () => {
    const list = props.list || [];

    if(list.length == 0) {
      return (
        <tr><td className="text-center" colSpan="2">Nenhum registro encontrado</td></tr>
      )
    }

    return list.map(todo => {

      let classTitle = 'align-middle '
      classTitle += todo.IsDone ? "table-warning" : classTitle
  
      let classButtons = 'text-center '
      classButtons += todo.IsDone ? "table-warning " : classButtons

      return (
          <tr key={todo.Id}>
            <td className={classTitle}>{todo.Title}</td>
            <td
              className={classButtons}
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
        )}
    );
  };

  return (
    <table className="table mt-3 table-sm table-hover table-sm table-bordered">
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
