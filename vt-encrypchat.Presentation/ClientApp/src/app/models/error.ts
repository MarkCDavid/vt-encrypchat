interface Error {
  error: string;
  exception?: {
    message: string;
    stackTrace: string;
  };
}
