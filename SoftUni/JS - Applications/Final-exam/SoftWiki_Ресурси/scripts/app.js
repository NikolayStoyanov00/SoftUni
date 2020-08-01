import controllers from '../controllers/index.js';

const app = Sammy('#root', function () {

    this.use('Handlebars', 'hbs');

    //Home
    this.get('#/home', controllers.article.get.dashboard);

    //User
    this.get('#/user/register', controllers.user.get.register);
    this.post('#/user/register', controllers.user.post.register);

    this.get('#/home/login', controllers.user.get.login);
    this.post('#/user/login', controllers.user.post.login);

    this.get('#/user/logout', controllers.user.get.logout);

    //Article
    this.get('#/article/create', controllers.article.get.create);
    this.post('#/article/create', controllers.article.post.create);
    this.get('#/article/details/:articleId', controllers.article.get.details);
    this.get('#/article/close/:articleId', controllers.article.del.close);

    this.get('#/article/edit/:articleId', controllers.article.get.edit);
    this.post('#/article/details/:articleId', controllers.article.put.edit);
});

(() => {

    app.run('#/home');

})();