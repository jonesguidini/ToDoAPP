import React from "react";
import Grid from "../template/grid";
import Iconbutton from "../template/iconbutton";

export default props => (
  <div role="form" className="todoForm">
    <div className="row">
      <Grid cols="12 9 10">
        <input
          id="title"
          className="form-control"
          placeholder="Adicione uma tarefa"
          onChange={props.handleChange}
          value={props.title}
        />
      </Grid>

      <Grid cols="12 3 2">
        <Iconbutton style="primary" icon="plus" onClick={props.handleAdd}></Iconbutton>
      </Grid>
    </div>
  </div>
);
