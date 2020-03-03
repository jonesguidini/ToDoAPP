import React from 'react'
import { Route, Switch, Redirect } from "react-router-dom";

import Todo from '../todo/todo'
import About from '../about/about'

export default props => (
    <Switch>
        <Route path="/todos" component={Todo} />
        <Route path="/about" component={About} />
        <Route path="/" exact component={Todo} />
    </Switch>

)