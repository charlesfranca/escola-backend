var api = {};

// Variaveis principais da api
api.basePath = "";
api.token = "";

// Endpoints
api.endpoints = {};
api.endpoints.categories = {
    basePath: "",
    newCategory: this.basePath + "",
    getAllCategories: this.basePath + "",
    deleteCategory: this.basePath + "",
}

api.endpoints.courses = {
    basePath: "",
    newCourse: this.basePath + "",
    getAllCourses: this.basePath + "",
    deleteCourse: this.basePath + "",
}
// Endpoints