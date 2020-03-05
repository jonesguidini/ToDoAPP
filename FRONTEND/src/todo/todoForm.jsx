import React from "react";
import Grid from "../template/grid";
import Iconbutton from "../template/iconbutton";

export default props => {
  const keyHandler = e => {
    if (e.key === "Enter") {
      e.shiftKey ? props.handleSearch() : props.handleAdd();
    } else if (e.key === "Escape") {
      props.handleClear();
    }
  };

  return (
    <div role="form" className="todoForm">
      <div className="row">
        <Grid cols="12 9 10">
          <input
            id="title"
            className="form-control"
            placeholder="Adicione uma tarefa"
            onChange={props.handleChange}
            value={props.title}
            onKeyUp={keyHandler}
          />
        </Grid>

        <Grid cols="12 3 2">
          <Iconbutton style="primary" icon="plus" onClick={props.handleAdd} />
          <Iconbutton style="info" icon="search" onClick={props.handleSearch} />
          <Iconbutton
            style="secondary"
            icon="close"
            onClick={props.handleClear}
          />
        </Grid>
      </div>
    </div>
  );
};
