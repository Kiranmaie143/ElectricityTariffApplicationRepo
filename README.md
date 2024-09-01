# Electricity WebApp Management steps (without docker file -first)
1. created server and client for back end and front end using c#(.net 8) rest web api and front end with react as per job description.I do have good experience with angular 12 or latest as well but according job description created in react .
2. created unit test as well to follow test driven development
3. Added seperation of concerns incase future if new tariff added or new product added with strategy pattern and added exception middleware to handle global exceptions
4. Displayed Ui in form with consumption and list of tariff details.
5. press F5 visual studio will launch backend url and front end url . add consumption value in Ui (ex 3500) and click on calculate button after that list will be displayed.

# Electricity WebApp Management steps (with docker file)

1. Enabled docker support in server project
2. created docker file in server project  . added in repo
3. executed commads like 
    docker build -t electricity-tariff-app -f ElectricityTariffTest.Server/Dockerfile .    
    docker run -d -p 8080:5000 electricity-tariff-app
    launch url with  http://localhost:5000/swagger or http://localhost:5000:8080/swagger

deploying in kubernetes  (didn't tried in  azure as don't have access so providing commands )
 az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --enable-addons monitoring --generate-ssh-keys
 az aks get-credentials --resource-group myResourceGroup --name myAKSCluster
 kubectl create deployment myapp --image=myContainerRegistry.azurecr.io/myapp:v1
 kubectl expose deployment myapp --type=LoadBalancer --port 80 --target-port 80
 kubectl get services
 
  

