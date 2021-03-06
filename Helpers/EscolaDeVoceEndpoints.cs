using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaDeVoce.Backend.Helpers
{
    public class EscolaDeVoceEndpoints
    {
        // public const string baseUrl = "http://localhost:5000/api/";
        // public const string tokenUrl = "http://localhost:5000/token";

        public const string baseUrl = "http://escola-api.azurewebsites.net/api/";
        public const string tokenUrl = "http://escola-api.azurewebsites.net/token";
        public class Category{
            public const string getCategories = EscolaDeVoceEndpoints.baseUrl + "categorias";
        }

        public class School{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "school";
        }

        public class Project{
            public const string getProjects = EscolaDeVoceEndpoints.baseUrl + "projects";
        }

        public class News{
            public const string getNews = EscolaDeVoceEndpoints.baseUrl + "news";
        }

        public class Courses{
            public const string getCourses = EscolaDeVoceEndpoints.baseUrl + "courses";
            public const string getDetails = EscolaDeVoceEndpoints.baseUrl + "courses/details";
        }

        public class Videos{
            public const string video = EscolaDeVoceEndpoints.baseUrl + "video";
        }

        public class Especialists{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "especialists";
        }
    }
}