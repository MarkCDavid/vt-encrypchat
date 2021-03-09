interface SignInRequest {
  username: string;
  password: string;
}


interface SignInResponse {
  success: boolean;
  error?: string;
}


