export class HttpClient {
    constructor(private baseUrl: string, private token: string | null) {
        // this.token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJkZjc3Nzc4Zi0yZWYzLTQ5YWYtYTFhOC1iMWYwNjQ4OTFlZjUiLCJlbWFpbCI6InRlc3R1c2VyQGd0ZHBhZC5jb20iLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTk1NjU3MTM5LCJleHAiOjE1OTU3NDM1MzksImlhdCI6MTU5NTY1NzEzOX0.bLKMXQKBjRjeWTm7B1LUprpi3TKN9otajNKXxdRBrDc';
    }

    public setBearerToken(token: string): void {
        this.token = token;
    }

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

