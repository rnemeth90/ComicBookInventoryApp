## In progress:
- [ ] Add sorting

## To do:
- [ ] Add API Load Testing
- [ ] Create Upload Service
- [ ] Add healthcheckUI
- [ ] Generic controllers in MVC? Maybe help reduce code
- [ ] In api, add functionality for uploading covers as blobs to storage
- [ ] Create SPA frontend (likely Angular)
- [ ] Change favicon for MVC
- [ ] We are handling errors in too many ways, need to streamline
- [ ] Add more properties to comic books, characters, and authors
- [ ] Can we invoke Marvel's API to get some info?

## Done:
#### 06/14/2022
- [x] Add serilog logging

#### 06/12/20222
- [x] Implement Paging
- [x] Implement Searching

#### 06/11/2022
- [x] Add API Versioning Header Support
- [x] IsRead should be a check box

#### 06/10/2022
- [x] Add model validation
- [x] Users should only be allowed to set book rating between 1 and 10

#### 06/08/2022
- [x] As a user, we should be able to specify Characters and Authors when creating a comic

#### 06/04/2022
- [x] Remove ID from view models presented in MVC app (we don't want to display these to the user)
- [x] Add edit action and view for characters in MVC

#### 06/03/2022
- [x] Add 'create' operation to all controllers in mvc app
- [x] Verify CRUD operations work for all object types from MVC