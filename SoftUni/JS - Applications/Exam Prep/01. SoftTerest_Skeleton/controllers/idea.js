import extend from '../utils/context.js';
import models from '../models/index.js';
import idea from '../models/idea.js';
import docModifier from '../utils/doc-modifier.js';

export default {
    get: {
        dashboard(context) {

            models.idea.getAll().then((response) => {
                const ideas = response.docs.map(docModifier);

                context.ideas = ideas;

                extend(context).then(function() {
                    this.partial('../views/idea/dashboard.hbs');
                });
            });

            
        },

        create(context) {
            extend(context).then(function() {
                this.partial('../views/idea/create.hbs')
            });
        },

        details(context) {
            const {ideaId} = context.params;

            models.idea.get(ideaId).then((response) => {

                const idea = docModifier(response);
                context.idea = idea;
                
                context.canComment = idea.uId !== localStorage.getItem('userId');

                extend(context).then(function() {
                    this.partial('../views/idea/details.hbs');
                })

            }).catch((e) => console.error(e));
        }
    },
    post: {
        create(context) {
            const data = {
                ...context.params, 
                uId: localStorage.getItem('userId'),
                likes: 0,
                comments: []
            };

            models.idea.create(data)
                .then((response) => {
                    context.redirect('#/idea/dashboard');
                    console.log(data);
                    console.log(response);
                })
                .catch((e) => console.error(e));
        }
    },
    del: {
        close(context) {
            const { ideaId } = context.params;
            models.idea.close(ideaId).then((reponse) => {
                context.redirect('#/idea/dashboard');
            })
        }
    },
    put: {
        comment(context) {
            const { ideaId } = context.params;
            let article = {...context.params};
            console.log(article);
            models.idea.get(ideaId).then((response) => {
                const idea = docModifier(response);
                idea.comments.push(`${localStorage.getItem('userEmail')}: ${context.params.newComment}`);

                return models.idea.put(ideaId, idea);
            }).then((response) => {
                context.redirect('#/idea/dashboard');
            });
        }
    } 
};