let db = firebase.firestore();

export default {
    create(data) {
        return db.collection('articles').add(data);
    },
    getAll() {
        return db.collection('articles').get();
    },
    get(id) {
        return db.collection('articles').doc(id).get();
    },
    close(id) {
        return db.collection('articles').doc(id).delete();
    },
    put(id, data) {
        return db.collection('articles').doc(id).update(data);
    }
}