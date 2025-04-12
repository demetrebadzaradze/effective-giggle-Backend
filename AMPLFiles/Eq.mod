var Chair >= 0; 
param Chairmaterial;  
param Chairprice;  
param Chairtime; 

var Table >= 0; 
param Tablematerial;  
param Tableprice;  
param Tabletime; 

subject to materialConstaint: Chair * Chairmaterial  + Table * Tablematerial  <= 1700;  
subject to timeConstaint: Chair * Chairtime  + Table * Tabletime  <= 160;  


maximize profit: Chair * Chairprice  + Table * Tableprice ;  

