const fs = require('fs')

module.exports = {
    outputDir: '../server/wwwroot',
    devServer: {
        https: {
          key: fs.readFileSync('C:/Utils/mkcert/localhost+2-key.pem'),
          cert: fs.readFileSync('C:/Utils/mkcert/localhost+2.pem'),
        },
        public: 'https://localhost:8080/'
    }
}