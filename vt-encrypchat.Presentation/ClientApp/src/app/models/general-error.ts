interface GeneralError {
  error: string;
  exception?: {
    message: string;
    stackTrace: string;
  };
}
