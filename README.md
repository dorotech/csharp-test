## Desafio Back-End

### JSONs para teste:

#### >> Paged book:
//filtro por titulo do livro
{
  "filter": {
    "value": "a arte da"
  },
  "pagination": {
    "itemsPerPage": 1,
    "pageNumber": 1
  }
}

#### >> Sign In:
{
	"email": "lol",
	"password": "123"
}


#### >> Insert book
{
  "id": 0,
  "title": "interestellar",
  "description": "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.",
  "publishedDate": "17/03/2016",
  "author": "Christopher Nolan",
  "genre": "sci-fi",
  "publishingCompanies": [
    "SARAIVA", "moDERna"
  ]
}

#### >> Update book:
<p> Informar o id e os campos que deseja atualizar <br /> OBS: Os campos vazios nao terao alteracao, o campo que estava antes se mantera</p> 
{
  "id": 2,
  "title": "",
  "description": "lalala",
  "publishedDate": "",
  "author": "",
  "genre": "",
  "publishingCompanies": [
	"unESco"
  ]
}
