import React from "react";
import { Route, Switch, Redirect, useLocation } from "react-router-dom";

import Todo from "../todo/todo";
import About from "../about/about";

export default props => (
  <Switch>
    <Route
      path="/todos/2/3?titleFilter=&orderByFilter=Title&TipoOrderBy=Ascending"
      exact
      component={Todo}
    />
    <Route path="/todos" component={Todo} />
    <Route path="/about" component={About} />
    <Route path="/" exact component={Todo} />
  </Switch>
);
