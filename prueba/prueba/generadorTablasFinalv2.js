  let sentencias = [];
  let textofinal="";
    const parseTable = () => {
      for (let i=1; i<=249;i++){
        //console.log((`INSERT INTO Tablas (table_id, condicion) VALUES ${i}, ""; \n `));
        sentencias.push(`INSERT INTO Tablas (table_id) VALUES (${i}); \n `);

      }

        
      };




      parseTable();
     
      
  
    
    for(let i=0;i<sentencias.length;i++){
        textofinal +=sentencias[i]; 

    }

    console.log(textofinal);


    //const queries = query;
    // Create element with <a> tag
    const link = document.createElement("a");

    // Create a blog object with the file content which you want to add to the file
    const file = new Blob([textofinal], { type: 'text/plain' });

    // Add file content in the object URL
    link.href = URL.createObjectURL(file);

    // Add file name
    link.download = "pruebaInsert.txt";

    // Add click event to <a> tag to save file.
    link.click();
    URL.revokeObjectURL(link.href);




