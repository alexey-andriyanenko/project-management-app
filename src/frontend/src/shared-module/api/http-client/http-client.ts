import { HttpMessage } from "./http-message";

type HttpClientInterceptor = (request: XMLHttpRequest) => Promise<void> | void;

type HttpClientConfig = {
  baseUrl: string;
  anonymous?: boolean;
  interceptors?: HttpClientInterceptor[];
  onTokenRefresh?: () => Promise<string | null>;
};

export class HttpClient {
  private _config: HttpClientConfig;
  private static _isRefreshing = false;
  private static _refreshQueue: Array<{
    resolve: (token: string | null) => void;
    reject: (error: object) => void;
    onTokenRefresh: (() => Promise<string | null>) | null;
  }> = [];

  constructor(_config: HttpClientConfig) {
    this._config = _config;
  }

  public static accessToken = localStorage.getItem("accessToken");

  public static refreshToken = localStorage.getItem("refreshToken");

  public static get isRefreshing(): boolean {
    return HttpClient._isRefreshing;
  }

  public static setRefreshing(value: boolean): void {
    HttpClient._isRefreshing = value;
  }

  public static addToRefreshQueue(): Promise<string | null> {
    return new Promise((resolve, reject) => {
      HttpClient._refreshQueue.push({ resolve, reject, onTokenRefresh: null });
    });
  }

  public static processRefreshQueue(error: unknown = null, token: string | null = null): void {
    HttpClient._refreshQueue.forEach(({ resolve, reject }) => {
      if (error) {
        reject(error);
      } else {
        resolve(token);
      }
    });
    HttpClient._refreshQueue = [];
  }

  public get interceptors(): HttpClientInterceptor[] {
    return this._config.interceptors ?? [];
  }

  public get onTokenRefresh(): (() => Promise<string | null>) | undefined {
    return this._config.onTokenRefresh;
  }

  public get<Response, Path extends string = string>(url: Path) {
    return HttpMessage.create<never, Response, "get", Path>(
      (this._config.baseUrl + url) as Path,
      "get",
      this,
    );
  }

  public post<Request, Response, Path extends string = string>(url: Path) {
    return HttpMessage.create<Request, Response, "post", Path>(
      (this._config.baseUrl + url) as Path,
      "post",
      this,
    );
  }

  public put<Request, Response, Path extends string = string>(url: Path) {
    return HttpMessage.create<Request, Response, "put", Path>(
      (this._config.baseUrl + url) as Path,
      "put",
      this,
    );
  }

  public delete<Response, Path extends string = string>(url: Path) {
    return HttpMessage.create<never, Response, "delete", Path>(
      (this._config.baseUrl + url) as Path,
      "delete",
      this,
    );
  }
}
