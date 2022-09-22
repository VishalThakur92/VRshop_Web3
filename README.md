🧭 **`Index`**
- [**Project Hierarchy**](#project-hierarchy)
- [**Scene Hierarchy**](#scene-hierarchy)
- [**Scripts Structure**](#scipts-structure)
- [**Asset Bundles**](#asset-bundles)
- [**Login**](#login)
- [**Shop**](#shop)
- [**Shop Products**](#shop-products)
- [**Shop Filters**](#shop-filters)
- [**Download Products**](#download-products)
- [**Product Interactions**](#product-interactions)
- [**Edit Existing Products**](#edit-existing-products)
- [**Add new Products**](#add-new-products)
- [**WEB3 and Metaverse**](#web3-and-metaverse)


# Project Hierarchy
![2](https://user-images.githubusercontent.com/16806053/191695721-bc173242-cb5e-4eb7-ac2e-66c2b197cad2.PNG)

Overview -
1. **`Animations`** - Animation-related components
1. **`Audio`** - Audio-related components
1. **`BundledAssets`** - Asset bundles to be built are placed in this folder
1. **`Materials`** - Custom materials
1. **`Plugins`** - Platform-specific libraries
1. **`Prefabs`** - Saved prefabs
1. **`RenderPipelines`** - Render pipelines that can be used for this project
1. **`Scenes`** - All scenes
1. **`Scripts`** - Custom scripts for this project
1. **`Shaders`** - Custom shaders
1. **`StreamingAssets`** - AssetBundles once built will be exported to this folder
1. **`Textures`** - Custom textures
1. **`ThirdParty`** - Thirdparty packages and plugins 
1. **`XR`** - XR related files




# **Scene Hierarchy**

![1](https://user-images.githubusercontent.com/16806053/191696467-87dd5eec-68d6-45b2-9250-56f9be858406.PNG)

Overview -
1. **`Core`** - Core logic scripts
1. **`Environment`** - 3D models in the scene
1. **`UI`** - UI elements
1. **`Audio`** - Audio elements




# **Scipts Structure**
![Capture](https://user-images.githubusercontent.com/16806053/191698482-03f50e89-8026-4cdc-853f-a7bb57f089ae.PNG)

Overview -
1. **`Core`** - Scipts that control the controlling algorithm of the application
1. **`Data`** - Scripts that contains static data asseccible by all Core scripts




# **Asset Bundles**
![3](https://user-images.githubusercontent.com/16806053/191704331-cc2da4f1-cff0-4e26-a07a-e99c6d2565c4.PNG)

Overview -
1. **BundledAssets**- Place all asset bundles to be built here
1. **StreamingAssets** - All asset bundles once built, are exported here

How to build Asset bundles:

On top menu select **`Assets/Build AssetBundles`** and they will be exported in the Streaming Assets Folder



# **Login**

![login](https://user-images.githubusercontent.com/16806053/191707087-267f37dc-49e1-4f0e-bcfd-f059167f6569.PNG)

You can login via the following methods -
1. **Guest Login**- You don't need to connect via wallet? No worries, you can just explore the app as a guest
1. **Wallet Connect** - Click Wallet Login, and the app shall show you a QR code, which you need to scan with your wallet app's scanner. Upon successful authentication you will be loaded intothe `Main` scene, and at the bottom left corner you can see the logged in wallet ID as shown below - 

![walletID](https://user-images.githubusercontent.com/16806053/191709095-7daaae8f-ec2c-4215-8b60-d4f330b63188.PNG)


Moreover, you can now see that the logged in user's data is being reflected in the Moralis Server DB as seen below -

![database](https://user-images.githubusercontent.com/16806053/191709364-b6cec135-d464-40f5-9ac3-35d6147b2b6b.PNG)
![database2](https://user-images.githubusercontent.com/16806053/191709398-d4948812-c0ef-4975-8168-d2a9886a063d.PNG)




# **Shop**

Once you are logged in, you can see a Shopping Cart Icon as see below, click on it using `Left Mouse Button` as seen below:

![Shop1](https://user-images.githubusercontent.com/16806053/191710180-da325d8b-3613-4015-8bf4-fda775c8be13.PNG)

As you click, you will see an interactive 3D Shop Canvas: 
![shop2](https://user-images.githubusercontent.com/16806053/191710687-68f68c54-49fc-47ed-8846-0c2f7b574f67.PNG)



# **Shop Products**

In the Shop Canvas, in the bottom you can see all the products as were specified in the JSON file placed over the cloud:

![products](https://user-images.githubusercontent.com/16806053/191711908-61c0fd2a-1e4d-4b1e-816e-89f32de9068c.PNG)


# **Shop Filters**

In the Shop Canvas, to the right you can see product filters which you can use to filter and show only the specific products. As you can see below, `Furniture` filter has been selected and the shop only shows the products which of type `Furniture`:

![filters](https://user-images.githubusercontent.com/16806053/191712296-17f9895d-e2cf-4777-b0e8-4dea1e873dde.PNG)



# **Download Products**
The products can be downloaded direclty from the cloud to the application's 3D space. To do so, select the desired product and then click `Buy`, the application will then download the asset bundle from the cloud and unpack it in front of you. You can then place it in the desired location and then do the desired interactions with it. 

![Buy](https://user-images.githubusercontent.com/16806053/191713311-26f2461b-72f6-478f-8576-5b0f3aa23780.png)




# **Product Interactions**
![interaction](https://user-images.githubusercontent.com/16806053/191714871-9a571fa3-2233-4d08-9ec3-8a8f8e39f8cb.PNG)
![interaction 2](https://user-images.githubusercontent.com/16806053/191715030-8dc9ace9-8d97-4528-a7e0-5c8d6beee68c.PNG)

Once a product has been downloaded, you can click on it and you will see the possible interactions. Currently the following interactions has been implemented:

- `Move` - You can place the product at the desired location, by looking around, to place the prduct click `Left Mouse Button`. For now the product can only be placed on the ground.
- `Rotate Left` - Rotate the product model to left by a certain degree
- `Rotate Right` - Rotate the product model to left by a certain degre
- `Scale Up` - Scale up the product model
- `Scale Down` - Scale down the product model
- `Special` - Plays a behaviour/animation that is unique to the product, for instance the bouncing ball will bounce like crazy and then return to it's origin after a certain period of time.
- `Close Menu` - Close the product's interactive canvas.




# **Edit Existing Products**
To edit product info you will need to edit the JSON files placed at  `Assets\StreamingAssets\CloudData\products.JSON`. The application does not fetch data from here, these files are just a replica of the folder structure over the cloud.

![JSON](https://user-images.githubusercontent.com/16806053/191717666-741e32e4-d26e-4b49-b6ca-08f83e2be463.PNG)

As seen above, the desired parameter of the product can be edited here. But, you wont be able to edit this, because the files are being hosted over my server. So you will need to tell me to make the change, unless the files are on your server.


# **Add new Products**
You can easily add a new downloadable product. Changes are required both on the client as well as the cloud, Have a look at the steps below -

- `Client Side Changes` 
1. Duplicate any existing product inside `Assets/BundledAssets` as seen below:
![newProduct2](https://user-images.githubusercontent.com/16806053/191722437-383329b2-5355-41bf-810b-d7c83ee4630c.PNG)
2. Open the product prefab as seen below:
![newProduct3](https://user-images.githubusercontent.com/16806053/191722706-9ea9a1c9-a678-4f57-b2ce-8877ed331aaa.PNG)
3. Change the `mesh` to what you like, Just update the `MeshRenderer` and `MeshFilter` components with the new ones.
4. You can Enable or Disable the exisitng interactions such as `Move`, `RotateLeft`, `ScaleDown`, `Special` etc.
5. You can specify what happens what happens on `Special` interaction is interacted by adding methods in the inspector as seen below : 
![product5](https://user-images.githubusercontent.com/16806053/191724539-603b14f0-7a7c-40c7-a550-986dc3bc9ec2.PNG)
6. Once done, the product's asset bundle name needs to be set, which can be done as seen below: 
![final](https://user-images.githubusercontent.com/16806053/191724845-4b7a96ae-dc72-41ed-a98e-f55573e3806c.png)
7. Now you are all set, you now need to [**Build Asset Bundles**](#asset-bundles)
8. You can follow the Cloud side change from here on.


- `Cloud Side Changes` - 
1. Add the product's icon inside `icons` folder
2. Add the product's asset bundle inside `assetBundles` folder
3. Add a new product object in the JSON file and fill in parameters. Also the URLs must be correct, otherwise the product shall not be loaded/downloaded in the scene as expected.

![newProduct](https://user-images.githubusercontent.com/16806053/191721700-ae01038b-7e9c-474d-8bfe-04731882aa36.PNG)




# **WEB3 and Metaverse**
The application has been injected with **Moralis** (https://moralis.io/) SDK with is a full blown WEB3 plugin, The application right now is just logging in user via wallet connect. It can be further expanded so that the app can have all the Metaverse bells and whistles.




