function loadPages() {
    //const formData = new FormData(loginForm);
    //const data = Array.from(formData).reduce((o, [k, v]) => (o[k] = v, o), {});
    //const payload = JSON.stringify(data);
    const request = new Request('api/pages', {
        method: 'GET',
        //body: payload,
        headers: new Headers({
            'Authorization': 'Bearer ' + GLOBAL_JWT,
            'Content-Type': 'application/json'
        })
    });
    fetch(request)
        .then(res => {
        if (res.status == 401) {
            loadLoginForm();
            return Promise.resolve();
        }
        return res.json().then(json => {
            const pages = json;
            const items = pages.sort(p => p.order).map(p => `<li data-id="${p.id}">${p.title}</li>`);
            document.getElementById('content').innerHTML = `<ul>${items.join('')}</ul>`;
        });
    })
        .catch(err => console.log(err));
}
loadPages();
//# sourceMappingURL=index.js.map