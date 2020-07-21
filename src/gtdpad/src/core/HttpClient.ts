export class HttpClient {
    constructor(private baseUrl: string, private token: string) {}

    public async httpGet<T>(url: string): Promise<T> {
        const request = new Request(this.baseUrl + url, {
            method: 'GET',
            headers: new Headers({
                'Authorization': 'Bearer ' + this.token
            })
        });

        return await this.makeRequest<T>(request);
    }

    public async httpPost<T>(url: string, body: object): Promise<T> {
        const request = new Request(this.baseUrl + url, {
            method: 'POST',
            body: JSON.stringify(body),
            headers: new Headers({
                'Authorization': 'Bearer ' + this.token,
                'Content-Type': 'application/json'
            })
        });

        return await this.makeRequest<T>(request);
    }

    public async httpPut<T>(url: string, body: object): Promise<T> {
        const request = new Request(this.baseUrl + url, {
            method: 'PUT',
            body: JSON.stringify(body),
            headers: new Headers({
                'Authorization': 'Bearer ' + this.token,
                'Content-Type': 'application/json'
            })
        });

        return await this.makeRequest<T>(request);
    }

    public async httpDelete<T>(url: string): Promise<T> {
        const request = new Request(this.baseUrl + url, {
            method: 'DELETE',
            headers: new Headers({
                'Authorization': 'Bearer ' + this.token
            })
        });

        return await this.makeRequest<T>(request);
    }

    private async makeRequest<T>(request: Request): Promise<T> {
        return await fetch(request)
            .then(response => !response.ok ? Promise.reject(response) : response)
            .then(response => response.json());
    }
}

