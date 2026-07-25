#include <iostream>
using namespace std;

int main() {
   
    // Variables que necesitamos
    int numero;
    int numeroActual;
    int cociente;
    int sobrante;
    int residuos[20];
    int contador;
    int posicion;
    string respuesta;
   
    // PARTE 1 - Recoleccion de datos
   
    cout << "Desea convertir un numero a sistema maya base 20? (si/no): ";
    cin >> respuesta;
   
    // Si dice no, terminar
    if (respuesta == "no" || respuesta == "NO" || respuesta == "No" || respuesta == "nO") {
        cout << "Gracias por usar la calculadora Maya" << endl;
        return 0;
    }
   
    // Si dice cualquier otra cosa, mostrar error y terminar
    if (respuesta != "si" && respuesta != "SI" && respuesta != "Si" && respuesta != "sI") {
        cout << "Solo se acepta Si o No" << endl;
        return 0;
    }
   
    // Si llegamos aqui, dijo si
    cout << "No se permite ingresar letras, el numero no debe contener espacios ni simbolos" << endl;
    cout << "Ingrese un numero: ";
    cin >> numero;
   
    // PARTE 2 - Proceso de calculo
   
    contador = 0;
   
    // Tomar el numero del usuario y llamarlo numero actual
    numeroActual = numero;
   
    // Si el numero actual es cero
    if (numeroActual == 0) {
        cout << "El resultado es cero" << endl;
    }
   
    // Si el numero actual no es cero
    if (numeroActual != 0) {
       
        // Repetir mientras el numero actual sea mayor que cero
        while (numeroActual > 0) {
           
            // Dividir el numero actual entre veinte
            cociente = numeroActual / 20;
            sobrante = numeroActual % 20;
           
            // El sobrante guardarlo en la lista de residuos en la posicion del contador
            residuos[contador] = sobrante;
           
            // Aumentar el contador en uno
            contador = contador + 1;
           
            // El numero actual ahora es el resultado entero de la division sin el sobrante
            numeroActual = cociente;
           
        } 
       
    } 
   
    // PARTE 3 - Presentacion de datos
   
    // Si el numero no era cero
    if (numero != 0) {
       
        cout << "El numero en base veinte maya es: ";
       
        posicion = contador - 1;
       
        // Mientras no lleguemos a la primera posicion
        while (posicion >= 0) {
           
            // Mostrar en pantalla el residuo que esta en esa posicion
            cout << residuos[posicion] << " ";
           
            // Pasar a la posicion anterior
            posicion = posicion - 1;
           
        } 
       
        cout << endl;
       
    } 
   
    // Preguntar al usuario si quiere ingresar otro numero o terminar el programa
    cout << "Quiere convertir otro numero? (si/no): ";
    cin >> respuesta;
   
    if (respuesta == "si" || respuesta == "SI" || respuesta == "Si" || respuesta == "sI") {
        cout << "Reinicie el programa para convertir otro numero" << endl;
    }
    else {
        cout << "Gracias por usar la calculadora Maya" << endl;
    }
   
    return 0;
}