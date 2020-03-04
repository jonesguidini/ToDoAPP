import React from "react";

export default props => (
  <header className="page-header mt-4">
    <h2>
      {props.name} <small>{props.small}</small>
    </h2>
    <hr></hr>
  </header>
);
