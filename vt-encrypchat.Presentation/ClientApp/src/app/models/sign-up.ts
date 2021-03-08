interface SignUpRequest {
  username: string;
  password: string;
}


interface SignUpResponse {
  success: boolean;
  error?: string;
}


