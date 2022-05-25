export const Currency = "NIS";
export const serverPort = "https://localhost:7242"; 
// TextField for example, expect to get this type of function on onChange Event:  (event: React.ChangeEvent<HTMLInputElement>) => void
// instead of making handler for each property that can be changed, this function generates the handlers (using the setState returned from React.useState())
export const makeSetStateFromEvent = (setState: any) => {
  return (event: React.ChangeEvent<HTMLInputElement>) => {
    setState(event.target.value);
  };
};
