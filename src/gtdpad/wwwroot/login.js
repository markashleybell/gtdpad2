let GLOBAL_JWT = null;
const loginForm = document.getElementById('login');
function loadLoginForm() {
    const request = new Request('home/login', {
        method: 'GET'
    });
    fetch(request)
        .then(res => res.text())
        .then(html => { document.getElementById('content').innerHTML = html; })
        .catch(err => console.log(err));
}
function login(form) {
    const formData = new FormData(form);
    const data = Array.from(formData).reduce((o, [k, v]) => (o[k] = v, o), {});
    const payload = JSON.stringify(data);
    const request = new Request(form.action, {
        method: 'POST',
        body: payload,
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });
    fetch(request)
        .then(res => res.json())
        .then(data => {
        GLOBAL_JWT = data.token;
        loadPages();
    })
        .catch(err => console.log(err));
}
document.addEventListener('submit', function (e) {
    e.preventDefault();
    const el = e.target;
    if (el.id == 'login') {
        login(el);
    }
});
//# sourceMappingURL=login.js.map