//Js del inicio//
var flag = false;
var buscaCurso = "";	
$('#body').fadeIn(1000,function(){ flag=true; });
$('#logo').click(function(){
if (flag == true){
	$('#botones').fadeIn(150);
	$('#logo').toggleClass('logo-change');
	$('#btnprofe').toggleClass('btnprofe');
	setTimeout(function(){$('#btncurso').toggleClass('btncurso')}, 100);
}
});	

$.History.bind('',function(state){

	if($('#profesoresCuadro').hasClass("profesoresCuadro-change")){
	$('#profesoresCuadro').removeClass(function() {
	return $(this).attr('class');
	});
    $('#profesoresCuadro').addClass("profesoresCuadro-inicio");
	setTimeout(function(){ 
	if (document.location.hash != "#/profesores" || document.location.hash != "#/profesores2"){
	$('#profesoresCuadro').addClass("esconder");
	$(window).resize();	
	}	
	},2500);
	}
	
	if($('#CursosWrap').hasClass("CursosWrap-change")){
	$('#CursosWrap').removeClass(function() {
	return $(this).attr('class');
	});
    $('#CursosWrap').addClass("CursosWrap-inicio");
	setTimeout(function(){ 
	if (document.location.hash != "#/cursos"){
	$('#CursosWrap').addClass("esconder");
	$(window).resize();	
	}	
	},2500);
	}
	
	
	$('#inicio').removeClass("esconder");
	setTimeout(function(){$('#inicio').addClass("mostrar")},100);
	setTimeout(function(){
	$('#inicio').removeClass(function() {
	return $(this).attr('class');
	});
	$('#inicio').addClass("inicio");
	$(window).resize();	
	},200);
	
	
});

$.History.bind('/profesores',function(state){
	$('#inicio').removeClass(function() {
	return $(this).attr('class');
	});
	$('#inicio').addClass("inicio-change");
	setTimeout(function(){ 
	if (document.location.hash != ""){
	$('#inicio').addClass("esconder");
	}	
	},2500);
	
	if($('#HoriarioWrap').hasClass("HoriarioWrap-change")){
	$('#HoriarioWrap').removeClass(function() {
	return $(this).attr('class');
	});
	limpiarHorario();
    $('#HoriarioWrap').addClass("HoriarioWrap-inicio");
	setTimeout(function(){ 
	if (document.location.hash != "#/Horario"){
	$('#HoriarioWrap').addClass("esconder");
	}	
	},2500);
	}
	
	
	
	$('#profesoresCuadro').removeClass("esconder");
	setTimeout(function(){$('#profesoresCuadro').addClass("mostrar")},100);
	setTimeout(function(){
	$('#profesoresCuadro').removeClass(function() {
	return $(this).attr('class');
	});
	$('#profesoresCuadro').addClass("profesoresCuadro-change");
	},200);
	
});

$.History.bind('/profesores2',function(state){
	$('#CursosWrap').removeClass(function() {
	return $(this).attr('class');
	});
	$('#CursosWrap').addClass("CursosWrap-out");
	setTimeout(function(){ 
	if (document.location.hash != "#/cursos"){
	$('#CursosWrap').addClass("esconder");
	}	
	},2500);
	
	if($('#HoriarioWrap').hasClass("HoriarioWrap-change")){
	$('#HoriarioWrap').removeClass(function() {
	return $(this).attr('class');
	});
	limpiarHorario();
    $('#HoriarioWrap').addClass("HoriarioWrap-inicio");
	setTimeout(function(){ 
	if (document.location.hash != "#/Horario"){
	$('#HoriarioWrap').addClass("esconder");
	}	
	},2500);
	}
	
	$('#profesoresCuadro').removeClass("esconder");
	setTimeout(function(){$('#profesoresCuadro').addClass("mostrar")},100);
	setTimeout(function(){
	$('#profesoresCuadro').removeClass(function() {
	return $(this).attr('class');
	});
	$('#profesoresCuadro').addClass("profesoresCuadro-change");
	},200);
	
});

$.History.bind('/Horario',function(state){
	$('#profesoresCuadro').removeClass(function() {
	return $(this).attr('class');
	});
	$('#profesoresCuadro').addClass("profesoresCuadro-out");
	setTimeout(function(){ 
	if (document.location.hash != "#/profesores" || document.location.hash != "#/profesores2"){
	$('#profesoresCuadro').addClass("esconder");
	}	
	},2500);
	
	
	$('#HoriarioWrap').removeClass("esconder");
	setTimeout(function(){$('#HoriarioWrap').addClass("mostrar")},100);
	setTimeout(function(){
	$('#HoriarioWrap').removeClass(function() {
	return $(this).attr('class');
	});
	$('#HoriarioWrap').addClass("HoriarioWrap-change");
	},200);
});

$.History.bind('/cursos',function(state){
	$('#inicio').removeClass(function() {
	return $(this).attr('class');
	});
	$('#inicio').addClass("inicio-change"); 
	setTimeout(function(){ 
	if (document.location.hash != ""){
	$('#inicio').addClass("esconder");
	}	
	},2500);
	
	if($('#profesoresCuadro').hasClass("profesoresCuadro-change")){
	$('#profesoresCuadro').removeClass(function() {
	return $(this).attr('class');
	});
    $('#profesoresCuadro').addClass("profesoresCuadro-inicio");
	setTimeout(function(){ 
	if (document.location.hash != "#/profesores" || document.location.hash != "#/profesores2"){
	$('#profesoresCuadro').addClass("esconder");
	}	
	},2500);
	}
	
	$('#CursosWrap').removeClass("esconder");
	setTimeout(function(){$('#CursosWrap').addClass("mostrar")},100);
	setTimeout(function(){
	$('#CursosWrap').removeClass(function() {
	return $(this).attr('class');
	});
	$('#CursosWrap').addClass("CursosWrap-change");
	},200);

});

$('#btnprofe').click(function(){
buscaCurso = "";
ordenar();
document.location.href = "#/profesores";
});
$('#btncurso').click(function(){
document.location.href = "#/cursos";
});
//JS Profesores
var ordenar = function(){
var sortQuery = $('#type-me').val();

var index = 1;
	$('.filter-me article').each(function(){
	var dataCurso = $(this).attr('curso');
	var dataSortValue = $(this).attr('data-sort');
	
	regC = new RegExp('\('+buscaCurso+'\)' , 'gi');
	
	reg = new RegExp('\('+sortQuery+'\)' , 'gi');
	index++;
	if (dataSortValue.match(reg) && (dataCurso.indexOf(buscaCurso)!==-1)) {
	$(this).delay(5+3*index).fadeIn(300);
	}
	else{
	$(this).delay(5+3*index).fadeOut(300);
	}
	});
};
function mostrarHorario(profesor, curso){
  var colores = ["#B0FFa8","#FFA8AC","#A8FFF6","#FFCDA8","#B1CBFF","#FFE1A8","#C4B2FF","#FFF0A8","#DFADFF","#FFFEA8","#FFA8D6","#E0FFA8"];
  var c=0;
  //Para cado curso
  for (var i=0;i<curso.length;i++){
    c++;
      if (c>8){
      c=0;   
      
      }
  //Inicio de la busqueda de horario por curso
  //Clases
  for (var key in profesoresJSON[profesor].cursos[curso[i]].horario){
    if (profesoresJSON[profesor].cursos[curso[i]].horario[key].inicio == 0 &&
    profesoresJSON[profesor].cursos[curso[i]].horario[key].fin == 0){
    }else{
    for (var j=profesoresJSON[profesor].cursos[curso[i]].horario[key].inicio; 
      j<profesoresJSON[profesor].cursos[curso[i]].horario[key].fin; j++){

      document.getElementById(key+j).innerHTML=profesoresJSON[profesor].cursos[curso[i]].nombre+" - "+profesoresJSON[profesor].cursos[curso[i]].seccion+" - "+profesoresJSON[profesor].cursos[curso[i]].horario[key].salon;
      document.getElementById(key+j).style.backgroundColor = colores[c];
      }      
      }
      
    }
  }
  for (var j=0; j< profesoresJSON[profesor].asesorias.length;j++){
    var temp = false;
      for (var k =0; k<curso.length ; k++){
        if (profesoresJSON[profesor].asesorias[j].curso==profesoresJSON[profesor].cursos[curso[k]].nombre){
          temp=true;
        }
          if (temp){
            c++;
                if (c>8){
                c=0; 
              }   
              for (var key in profesoresJSON[profesor].asesorias[j].horario){
                if (profesoresJSON[profesor].asesorias[j].horario[key].inicio == 0 &&
                profesoresJSON[profesor].asesorias[j].horario[key].fin == 0){
                }else{
                 for  (var l=profesoresJSON[profesor].asesorias[j].horario[key].inicio; 
                  l<profesoresJSON[profesor].asesorias[j].horario[key].fin; l++){
                    document.getElementById(key+l).innerHTML="Asesoria - "+profesoresJSON[profesor].asesorias[j].curso+" - "+profesoresJSON[profesor].asesorias[j].horario[key].salon;
                    document.getElementById(key+l).style.backgroundColor = colores[c];
                 }
                         
              }                
            }
          }
      }
  }
}
function limpiarHorario(){
  for (var i=7;i<22;i++){
    document.getElementById("l"+i).innerHTML= "&nbsp";
    document.getElementById("m"+i).innerHTML= "&nbsp";
    document.getElementById("x"+i).innerHTML= "&nbsp";
    document.getElementById("j"+i).innerHTML= "&nbsp";
    document.getElementById("v"+i).innerHTML= "&nbsp";
    document.getElementById("s"+i).innerHTML= "&nbsp";
    document.getElementById("l"+i).style.backgroundColor= "";
    document.getElementById("m"+i).style.backgroundColor= "";
    document.getElementById("x"+i).style.backgroundColor= "";
    document.getElementById("j"+i).style.backgroundColor= "";
    document.getElementById("v"+i).style.backgroundColor= "";
    document.getElementById("s"+i).style.backgroundColor= "";
  }
}
function cargarcursos(){
        var cursoi = 2;
        var cursosJ = document.createElement("div");  
        cursosJ.id = "cursosJ";
        for (var key in cursosjson)
        {       
            cursoi++;   
            var contenedor = document.getElementById("contenedor-cursos");
            var ciclo = document.createElement("div");     
            var input = document.createElement("input");
            ciclo.className = "ciclo";
            input.id = "ac-" + cursoi;
            input.name="accordion-1";
            input.type = "radio";                    
            var label = document.createElement("label");
            label.htmlFor = "ac-" + cursoi;
            label.innerHTML = cursoi + "° Ciclo";
            var article = document.createElement("article");

            switch(cursoi){
              case 3: article.className = "ac-small";
              break;
              case 4: article.className = "ac-special4";
              break;
              case 5: article.className = "ac-special4";
              break;
              case 6: article.className = "ac-special6";
              break;
              case 7: article.className = "ac-special7";
              break;
              case 8: article.className = "ac-large";
              break;
              case 9: article.className = "ac-large";
              break;
              case 10: article.className = "ac-large";
              break;
            }
            
            

            
            var ul = document.createElement("ul");
            for (var cursoj = 0; cursoj<cursosjson[key].length; cursoj++) {
                var li = document.createElement("li");
                li.value = cursosjson[key][cursoj] ;
                li.id = cursosjson[key][cursoj] ;
                li.innerHTML = cursosjson[key][cursoj] ;
                ul.appendChild(li);
                var cur =document.createElement("li");
                cur.className="filter";
                cur.id=cursosjson[key][cursoj];
                cur.innerHTML=cursosjson[key][cursoj];
                document.getElementById("contenedor-cursos").appendChild(cur);

            };                
            ciclo.appendChild(input);
            ciclo.appendChild(label);
            article.appendChild(ul);
            ciclo.appendChild(article);
            cursosJ.appendChild(ciclo);
            contenedor.appendChild(cursosJ);
            }
            document.getElementById('menu_cursos').appendChild(contenedor);
        }
$(document).ready(function() {
cargarcursos();

$(window).resize(function() {	
        $('#wrap').css({
            position: 'absolute',
            left: ($(window).width() - $('#wrap').outerWidth()) / 2,
            top: ($(window).height() - $('#wrap').outerHeight()) / 2
        });

    });
$(window).resize();	
	
var contenedor = document.getElementById("profimagenes");
for (i = 0 ; i<profesoresJSON.length;i++){
	var article = document.createElement("article");
	article.className = "profesor";
	article.setAttribute("data-sort", profesoresJSON[i].nombre); 
	article.setAttribute("data-id", i); 
	article.onclick = function(){ 
			//elegir profesor
			var id = this.getAttribute("data-id")
			var curs= "" ;
			var cursArr = new Array();
			
			if (buscaCurso == ""){
			for (a=0; a<profesoresJSON[id].cursos.length;a++){
			curs += " | " + profesoresJSON[id].cursos[a].nombre;
			cursArr[a] = a;
			}
			
			}else{
			var cont = 0;
				
				for (a=0; a<profesoresJSON[id].cursos.length;a++){
					if (buscaCurso == "|"+profesoresJSON[id].cursos[a].nombre+"|"){
					curs = " | " + profesoresJSON[id].cursos[a].nombre;
					cursArr[cont] = a;
					cont++;
			
					}
				}
			}
			// id == profe || cursosIDs == cursArr
			
			mostrarHorario(id,cursArr);
			
			document.location.href = "#/Horario";
			
			
    };
		
	var figure = document.createElement("figure");
	var imagen = document.createElement("img");
	var aux = "";
	aux = profesoresJSON[i].foto;
	if (!aux){
	aux = "profesor.jpg";
	}
	imagen.src = "images/fotos/"+aux;
	var nombre = document.createElement("p");
	nombre.innerHTML = profesoresJSON[i].nombre;
	
	var cadenaCursos = "";
	for (a=0; a<profesoresJSON[i].cursos.length;a++){
	cadenaCursos += "|"+profesoresJSON[i].cursos[a].nombre+"|";
	}
	
	article.setAttribute("curso", cadenaCursos);
	
	figure.appendChild(imagen);
	figure.appendChild(nombre);
	article.appendChild(figure);
	contenedor.appendChild(article);

}
var checkid = "nada";
$('#cursosJ input').on('click',function(){

	if(checkid == $(this).attr("id")){
	document.getElementById($(this).attr("id")).checked = false;
	}
	checkid = $(this).attr("id");	
});

$('#menu_cursos li').on('click',function(){
	var nom = $(this).attr('id');
	buscaCurso = "|"+nom+"|";
	ordenar();	
	document.location.href = "#/profesores2";
	
});




$('#type-me').ready(ordenar);
$('#type-me').keyup(ordenar);

var normalize = (function() {
			  var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
				  to   = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
				  mapping = {};
			 
			  for(var i = 0, j = from.length; i < j; i++ )
				  mapping[ from.charAt( i ) ] = to.charAt( i );
			 
			  return function( str ) {
				  var ret = [];
				  for( var i = 0, j = str.length; i < j; i++ ) {
					  var c = str.charAt( i );
					  if( mapping.hasOwnProperty( str.charAt( i ) ) )
						  ret.push( mapping[ c ] );
					  else
						  ret.push( c );
				  }
				  return ret.join( '' );
			  }
			 
		})();

$('#type-meBuscador').keyup(function(){
                    if($(this).val().length >= 1){
                        var sortQuery = $(this).val();
                    $('.ciclo').css("display","none");
                    $('.ac-container li').each(function(){
                        var dataSortValue = $(this).attr('id');
                        reg = new RegExp('\('+normalize(sortQuery)+'\)' , 'gi');
                        if (normalize(dataSortValue).match(reg)) {
                            $(this).fadeIn(300); 
                            }
                            else{
                            $(this).fadeOut(300); 
                        }
                    });
                    }else{
                        $('.filter').fadeOut(100);
                        $('.ciclo').fadeIn(300);
                    }

                    
                });

});
