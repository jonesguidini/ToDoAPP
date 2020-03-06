import React, { Component } from "react";
import _ from "lodash"; // conhecido como 'underscore'

const Pagination = props => {
  const { currentPage, onPageChange, totalPages } = props;

  const nextPage = currentPage < totalPages ? currentPage + 1 : currentPage
  const previousPage = currentPage > 1 ? currentPage - 1 : currentPage

  if (totalPages === 1) return null; // caso a qtd de itens retorne apenas uma pagina não montamos a paginação
  const pages = _.range(1, totalPages + 1); // cria array de pages , pagesCount precisa ser somado um pq a contagem de um array sempre começa do indice 0 em vez de 1

  return (
    <nav aria-label="Page navigation example">
      <ul className="pagination">
      <li key="111" className={"page-item"} ><a className="page-link" style={{ cursor: "pointer" }} onClick={() => onPageChange(previousPage)} >Anterior</a></li>
        {pages.map(page => (
          <li key={page} className={page === currentPage ? "page-item active" : "page-item"}>
            <a className="page-link" style={{ cursor: "pointer" }} onClick={() => onPageChange(page)}>{page}</a>
          </li>
        ))}
      <li key="222" className={"page-item"} ><a className="page-link disabled" style={{ cursor: "pointer" }} onClick={() => onPageChange(nextPage)} >Seguinte</a></li>
      </ul>
    </nav>
  );
};

export default Pagination;
