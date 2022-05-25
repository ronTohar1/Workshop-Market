export default interface ClientResponse<T> {
    errorMessage: string;
    value: T;
    errorOccured: boolean;
  }