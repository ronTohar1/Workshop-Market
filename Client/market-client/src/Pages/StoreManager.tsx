import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridToolbarDensitySelector,
  GridColDef,
} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import Navbar from "../components/Navbar";

import Product from "../DTOs/Product";
import { fetchProducts } from "../services/ProductsService";
import { useQueryParam, NumberParam, StringParam } from "use-query-params";
import StorePage from "./StorePage";

function ProductsTable() {
  return (
    <GridToolbarContainer>
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  );
}

export default function StoreManagerPage() {
  const [query] = useQueryParam("query", StringParam);

  const columns: GridColDef[] = [
    { field: "name", headerName: "Product", flex: 2 },
    { field: "price", headerName: "Price", flex: 1 },
    { field: "category", headerName: "Category", flex: 1 },
    { field: "store", headerName: "Store", flex: 1 },
  ];

  // map products to Map indexing items by id
  const productsLst = fetchProducts(query || "");
  const products: Map<number, Product> = Object.assign(
    {},
    ...productsLst.map((p: Product) => ({ [p.id]: p }))
  );

  type ProductRowType = {
    id: number;
    name: string;
    price: number;
    category: string;
    store: number;
  };

  let productsRows: ProductRowType[] = [];

  for (const p of productsLst) {
    productsRows.push({
      id: p.id,
      name: p.name,
      price: p.price,
      category: p.category,
      store: p.store,
    });
  }

  return (
    <Box>
      <Navbar />
      <div style={{ height: 700, width: "100%" }}>
        <StorePage />
      </div>
    </Box>
  );
}

StoreManagerPage.defaultProps = {
  query: "",
};

// export default SearchPage;
