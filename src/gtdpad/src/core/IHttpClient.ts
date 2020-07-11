export interface IHttpClient {
    httpGet<T>(url: string): Promise<T>;

    httpPost<T>(url: string, body: object): Promise<T>;

    httpPut<T>(url: string, body: object): Promise<T>;

    httpDelete<T>(url: string): Promise<T>;
}
