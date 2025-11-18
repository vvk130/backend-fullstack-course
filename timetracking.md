
| Date       | Time started | Time used  | Task               |
|------------|--------|--------|----------------------------------|
| 2025-10-02 | 13:00  | 2h | Started project, configuration, trying deployment to monsteraspnet with github direct and github actions, no 403 error anymore - but the swagger is not visible  |
| 2025-10-02 | 15:20  | 0,2h | Final restart of project in root, getting deployment to work: http://fullstackbackend.runasp.net/swagger/index.html |
| 2025-10-02 | 15:55 | 0,5 | Add postgres database - start by creating db in Aiven, install packages, add env variabels, update dbcontext and program.cs file, add port to successfully connect to Aiven (not usual port for portgres) |
| 2025-10-03 | 19:20 | 0,7h | Horse class |
| 2025-10-03 | 20:20 | 0,25h | Horse class modifications, primary keys |
| 2025-10-03 | 20:35 | 0,2h | Studying difference between owned and complex types in Ef core, no collection support for Complex type so using Owned type for this class |
| 2025-10-03 | 21:25 | 0,33h | Adjusting horse model, getting complex type to work, adding System.ComponentModel.DataAnnotations.Schema |
| 2025-10-03 | 21:45 | 0,4h | Adding things to horse model, competition model, competition dto |
| 2025-10-03 | 22:09 | 0,25h | Changing to controllers |
| 2025-10-04 | 10:00 | 1h | Changing to owned types in personality and fears in horse model. Problem with owned types, they were mapped wrong studying how to map them, needed to create a new database to run the migration (chosen database did not give access to pgconsole etc), finding a new database host koyeb and setting up database |
| 2025-10-04 | 11:04 | 0,25h | Starting horse creation service and interface for it |
| 2025-10-04 | 12:14 | 0,5h | Studying default api result types in net and validators and differenced in the frontend when consuming the api based on apporach |
| 2025-10-04 | 13:32 | 1h | Reading about bogus library, adding capitalization to name and removing colors that where in # format, creating random name generator with the library and creating the endpoint (maybe todo add gender overload?)|
| 2025-10-04 | 19:00 | 1h | Random breed height for horse, Model, service, interface and added to HorsesController - not placed in db because the free instances can spin down or inactivate etc |
| 2025-10-04 | 20:00 | 0,75h | Stable model, starting doing random horse creator |
| 2025-10-07 | 18:00-19:50 | 1,83h | Study Ef.BulkInsert, add package, fix db connection (since outside memory didn't and have to reset on pc), planning validators that are compatible with bulkinsert(attributes or will the library from fluentannotations work...), creating all horses (no pagination only development) endpoint, random color by brees endpoint, adding color data to horse breeds, horse creation and breed service updated |
| 2025-10-07 | 19:50-20:40 | 0,83h | Add age to horsemodel, add ownerId, create random horse creator |
| 2025-10-07 | 20:40-21:45 | 1,08 | Add more randomness to horsemodel (needs refactoring!), starting up with the bulkinsertions problems with the package - maybe wrong project type? Not working |
| 2025-10-07 | 21:45-22:45 | 1h | Reading documentation for the bulk update, trying a few things and reading documentation. Unclear if it will work with this hosted db type|
| 2025-10-07 | 22:45-23:00 | 0,25h | Using ExecuteUpdate for the batch update, since problems with the bulk library, controller and service function done|
| 2025-10-08 | 11:30 | 2 | Add owner, sire, damId, add aging update (for worker task), FoalService, FoalController (needs refactoring), Foal Interface|
| 2025-10-08 | 14:25 | 1,08 | FoalService to inherit some features from parents, breed, color, height, capacity inherited, age O, gender not gelding, corrected a mistake in the HorseBreedService GetRandomColorForBreed |
| 2025-10-08 | 16:15 | 1h | Add env variables to the moster api hosting, Level Model, some refactoring, exposing horse aging endpoint, researching where to host celery for free, render does not seem to offer it |
| 2025-10-08 | 17:15 | 0,33h | Reading and reasearching background task hosting options that are free, Hangfire? |
| 2025-10-08 | 19:51 | 1h | Adding colors to breeds, refactoring functions(removed async, naming issues etc), reading hangfire documentation, setting up hangfire background queue for horse aging and energy updates (due to the free hosting limitations on moster asp net, the functionality is similar to api, since the instance goes idle after 30 min, no recurring task would work with this hosting provider in free tier)  |
| 2025-10-08 | 21:30 | 1h | Studying LavinMQ and Cloudinary ImageUpload |
| 2025-10-09 | 17:20 | 1,5h | Cloudinary ImageUpload Ready |
| 2025-10-09 | 20:25 | 1h | GenericRepo and Interface |
| 2025-10-09 | 21:25 | 2h | GenericService and Interface, Controller for Horse Searching, Generic Controller |
| 2025-10-10 | 14:15 | 2,5h | Studying generic controller, adding generic controller, moving horse breeds to db, adding sire/damid/image to foalCreate(), adjusting models to use Guid because I'm too lazy to overload the Generic Find Id method (better to use UID etc), removing by overriding the delete methods from some models, adding images to use the models in front end, Starting Validators for Level and HorseBreed |
| 2025-10-10 | 18:25 | 2,5h | Studying and seetting up RabbitMQ CLOUDAMQP for foal notifications |
| 2025-10-11 | 9:13 | 1h | Changing to db because free instance ran out, first option did not work with ef core, second only on www and maybe not localhost still need to set up an local db |
| 2025-10-11 | 10:15 | 1,75h | Install docker, do a local database with docker | 
| 2025-10-13 | 10:30 | 2,5h | Login endpoints |
| 2025-10-13 | 13:30 | 2h | Adding roles, problem mix up with the login endpoints, not getting the right status code, finally fixed |
| 2025-10-13 | 15:30 | 2h | Generic mapping with automapper, getting an error (In the end resolved different package versions causing an error) |
| 2025-10-13 | 17:35 | 2h | Puzzle |
| 2025-10-15 | 11:30 | 2,5h | Puzzle and refactoring |
| 2025-10-15 | 15:00 | 1,5h | Puzzle and refactoring |
| 2025-10-15 | 17:30 | 2h | Competition service, endpoint |
| 2025-10-15 | 19:30 | 1,5 | Wallet service, pay comptition winners |
| 2025-10-15 | 21:30 | 1 | Refactoring, starting SalesAd, doing Wallet, thinking about the mapping types of FKs, thinking about best way to extract the user Id when using IdentityCore |
| 2025-10-17 | 13:30 | 3 | SalesAd bug, get set missing, trying to solve problems with the login hadling |
| 2025-10-17 | 22:10 | 1,25 | CompetitionService adding complexity |
| 2025-10-19 | | 3 | CompStats |
| 2025-10-19 | | 2 | CompStats Pagination, problems with the anonym type mapping |
| 2025-10-19 | | 3 | Buy horse endpoint |
| 20-10-2025 | | 1 | Filtering pagination |
| 20-10-2025 | | 3 | Auth problem, wrong config (?), has worked sometimes, "restart" the db or change system |
| 21-10-2025 | | 1 | Generic Update Endpoint |
| 21-10-2025 | | 2 | Studying mapping, adding Questions |
| 22-10-2025 | 13:30 | 2,5 | CreateEntityDtos withoutId field, validation for Questions, refactoring
| 22-10-2025 |  | 3 | Add CancellationToken CompetitionService, Ordering in pagination, refactoring
| 22-10-2025 | | 1 | Horse search, refactoring
| 22-10-2025 | | 1 | ImgStock
| 22-10-2025 | | 0,75 | Fixing bugs in code
| 23-10-2025 | |  | Time tracked on frontend
| 23-10-2025 | | 1 | CleanStablePayUser endpoint, fixing some bugs
| 24-10-2025 | |  | Time tracked on frontend
| 25-10-2025 | | 2 | Innerjoin for salesAds and horses, adding some indexing to db, some refactoring, usercontroller endpoint
| 25-10-2025 | | 3 | Fixing bug in buy horses controller, Auction functionality
| 26-10-2025 | | 3 | Alpaca model, sales ad to generic repo starting with does not work yet
| 26-10-2025 | | 2 | generic repo mapping problem
| 26-10-2025 | | 2,5 | refactoring
| 26-10-2025 | | 2 | Alpacas, generic animal model for salesAd creation and selling //rename HorseId to AnimalId
| 28-10-2025 | | 3 | Fixing Bug in the generate ad with Set<Animal>
| 28-10-2025 | | 2 | Fixing Bug in the generate ad and buy Animal
|03-11-2025 | | 0,5 | Validators
|03-11-2025 | | 1 | trying to fix automapper inheritance problem
|03-11-2025 | | 1 | fixing the nested inherited validator
|05-11-2025 | | 0,5 | validators
|11-11-2025 | | | refactoring, timetracked in front end repo
|17-11-2025|

https://docs.automapper.io/en/stable/Mapping-inheritance.html

Total time spent: 69,7h
17,5h per credit

# TODO

- [X] Filtering paginated response
- [ ] Suppress null errors
- [x] Ordering in pagination (Upgrade version?) ✅
- [ ] Competition service overfetching fix (fetches whole horse model?)
- [ ] Docker setup
- [x] Host on Netlify
- [x] Add CancellationToken support ✅
- [] Competition add horses in separate fields
- [ ] Prepare documentation for the project
- [ ] Get ready for submission

# Project features
|--------|----------------------------------|
|||

Palautus checkpoints:
[] Horse add, edit, delete
[] Alpaca add, edit, delete
[] Market ad add, edit, delete
[] Compete horses
[] Check comp stats
[] Wallet add remove money
[] Upload image

[] Last step: ilmo / lomake /email ✅
[] Remove comments, not used code ✅
[X] Deploy ✅
[] Add total hours, link to timetracking ✅
[] Add link to frontend repo ✅