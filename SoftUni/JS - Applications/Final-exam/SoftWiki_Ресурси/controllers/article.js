import extend from '../utils/context.js';
import models from '../models/index.js';
import article from '../models/article.js';
import docModifier from '../utils/doc-modifier.js';

export default {
    get: {
        dashboard(context) {

            models.article.getAll().then((response) => {

                const articles = response.docs.map(docModifier);
                
                let jsArticles = [];
                let csharpArticles = [];
                let javaArticles = [];
                let pytonArticles = [];


                Object.keys(articles).forEach((articleIndex) => {
                    let article = articles[articleIndex];

                    if (article.category === 'JavaScript') {
                        jsArticles.push(article);
                    } else if (article.category === 'CSharp' || article.category === 'C#') {
                        csharpArticles.push(article);
                    } else if (article.category === 'Java') {
                        javaArticles.push(article);
                    } else if (article.category === 'Pyton' || article.category === 'Python') {
                        pytonArticles.push(article);
                    }
                });

                let totalArticles = [];
                while (true) {
                    if (jsArticles.length > 0) {
                        totalArticles.push(jsArticles.pop());
                    } 

                    if (csharpArticles.length > 0) {
                        totalArticles.push(csharpArticles.pop());
                    }

                    if (javaArticles.length > 0) {
                        totalArticles.push(javaArticles.pop());
                    }

                    if (pytonArticles.length > 0) {
                        totalArticles.push(pytonArticles.pop());
                    }

                    if (jsArticles.length === 0 && csharpArticles.length === 0 && javaArticles.length === 0 && pytonArticles.length === 0) {
                        break;  
                    }
                }


                context.articles = articles;

                extend(context).then(function() {
                    this.partial('../views/home/home.hbs')
                });
            });
        },

        create(context) {
            extend(context).then(function() {
                this.partial('../views/article/create.hbs')
            });
        },

        details(context) {
            const {articleId} = context.params;

            models.article.get(articleId).then((response) => {

                const article = docModifier(response);
                context.article = article;
                
                context.canDelete = article.uId === localStorage.getItem('userId');

                extend(context).then(function() {
                    this.partial('../views/article/details.hbs');
                })

            }).catch((e) => console.error(e));
        },

        edit(context) {
            const {articleId} = context.params;
            
            context.articleId = articleId;

            extend(context).then(function() {
                this.partial('../views/article/edit.hbs')
            });
        }
    },
    post: {
        create(context) {
            const data = {
                ...context.params, 
                uId: localStorage.getItem('userId'),
            };

            models.article.create(data)
                .then((response) => {
                    context.redirect('#/home');
                })
                .catch((e) => console.error(e));
        }
    },
    del: {
        close(context) {
            const { articleId } = context.params;
            models.article.close(articleId).then((response) => {
                context.redirect('#/home');
            })
        }
    },
    put: {
        edit(context) {
            const { articleId } = context.params;
            let article = {...context.params};
            
            models.article.get(articleId).then((response) => {
                return models.article.put(articleId, article);
            }).then((response) => {
                context.redirect('#/home');
            });
        }
    } 
};