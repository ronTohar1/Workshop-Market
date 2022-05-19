import React from 'react'
import PropTypes from 'prop-types'
import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import CustomToolbarGrid  from '../components/ProductsList'

const Search = ({ query } : {query : string}) => {
  return (
    <DataGrid 
    {...[5]}
    components={{
      Toolbar: GridToolbar,
    }}
  />
  )
}

Search.defaultProps = {
  query: ""
}

export default Search

