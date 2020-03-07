import React from "react";

const TableInfo = props => {
  const { pageSize, totalPages, totalData } = props;
  return <div className=" text-left">Total de registros: {totalData}</div>;
};

export default TableInfo;
