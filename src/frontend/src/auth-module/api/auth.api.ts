import { appHttpClient } from "src/shared-module/api";

import type {
  LoginRequest,
  LoginResponse,
  RefreshTokenRequest,
  RefreshTokenResponse,
  RegisterRequest,
  RegisterResponse,
  UpdateUserRequest,
  UpdateUserResponse,
} from "./auth.types";
import type { UserModel } from "src/auth-module/models";

class AuthApiService {
  login(data: LoginRequest) {
    console.log(import.meta.env);
    return appHttpClient.post<LoginRequest, LoginResponse>("/auth/login").send(data);
  }

  loginWithRefreshToken(data: RefreshTokenRequest) {
    return appHttpClient
      .post<RefreshTokenRequest, RefreshTokenResponse>("/auth/refresh-token")
      .send(data);
  }

  register(data: RegisterRequest) {
    return appHttpClient.post<RegisterRequest, RegisterResponse>("/auth/register").send(data);
  }

  getMe() {
    return appHttpClient.get<UserModel>("/users/me").send();
  }

  updateMe(data: UpdateUserRequest) {
    return appHttpClient.put<UpdateUserRequest, UpdateUserResponse>("/users/me").send(data);
  }
}

export const authApiService = new AuthApiService();
