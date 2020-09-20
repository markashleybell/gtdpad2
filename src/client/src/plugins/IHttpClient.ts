export interface IHttpClient {
    isAuthenticated(): boolean;

    setBearerToken(token: string): void;

    httpGet<T>(url: string): Promise<T>;

    httpPost<T>(url: string, body: object): Promise<T>;

    httpPut<T>(url: string, body: object): Promise<T>;

    httpDelete<T>(url: string): Promise<T>;
}
