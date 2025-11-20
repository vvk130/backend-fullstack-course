## About the application

Game application where you can buy horses and alpacas and sell them

Net backend: uses FluentValidation, Generic Controller, for deleting, adding, editing Items and fetching paginated list
Animal baseclass for Alpaca and Horse

## Register and log in
Start by going to the homepage, register and then log in. You need a strong password and unique username. (There was a problem with the net login endpoints, so basically it just creates a user for you.)
![alt text](image.png)

## Create horse / create alpaca

Go to your alpacas and create and alpaca. Same for horses. 

![alt text](image-16.png)

## Change image for a horse

Go to images page, and copy url of an image, go back to horse individual page and update the imageUrl

![alt text](image-14.png)
![alt text](image-15.png)

## Clean the stable

Click the link Clean Stable in the navigation, and then click the button to clean the stable. Do you get some money for doing it? No. Maybe you get some good karma, who knows? 

![alt text](image-13.png)

## Update horse/alpaca

Go to an alpaca's page, select the Update tab, and update a field of your choice. The serverside validation let's you know if the data you input is valid. Horses have less field's that can be updated.  

![alt text](image-10.png)

## Sell horse / alpaca

You can sell horses or alpacas that you own, that are not already for sale. Go to your horses pick an id and paste to form. You get your userId from the bar below navigation. The ItemType needs to be Horse or Alpaca, depending on which animal you are selling. 

![alt text](image-4.png)

## Buy horse / alpaca

Then you can buy the animal you just bought by clicking the Buy/bid link. Then fill out the form with correct price and ItemType. If the ad was NOT an auction, it should disappear from the list. If the ad is a Auction type, the price is updated to the higher price. In Auctions the bid needs to be 200 higher than the current price of the ad. If the ad is not an auction, the bid amount will be ignored. 

![alt text](image-5.png)

![alt text](image-6.png)

## Create competition

Go to competitions page, click link create new competition, then fill out the form

![alt text](image-1.png)

## Update competition

Choose competition to update, edit the competition and follow the serverside validation guidelines

![alt text](image-11.png)

![alt text](image-12.png)

## Compete horses

Go to competitions, pick the competition and paste 3 horse ids in the fields (you don't have to own horses). Send the same form 3x times, to get better leaderboard results.

![alt text](image-2.png)

## Check the competition LeaderBoard

Check the horse Guids to show up on the leaderboard

![alt text](image-3.png)

## Delete horse, delete alpaca

Go to an animal's page, click Delete, and then click delete button. There is no cascading delete implemented, so salesads and leaderboard results are shown even though the animal has been deleted. Therefore it is recommended to do the deletions as a last step.

![alt text](image-7.png)

![alt text](image-8.png)

![alt text](image-9.png)