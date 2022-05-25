export default interface Response<T> {
    errorMessage: string;
    value: T;
    errorOccured: boolean;
  }