/**
 * oscP5parsing by andreas schlegel
 * example shows how to parse incoming osc messages "by hand".
 * it is recommended to take a look at oscP5plug for an
 * alternative and more convenient way to parse messages.
 * oscP5 website at http://www.sojamo.de/oscP5
 */

import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;

void setup() {
  size(200,200);
  frameRate(45);
  /* start oscP5, listening for incoming messages at port 12000 */
  oscP5 = new OscP5(this,8001);
  
  /* myRemoteLocation is a NetAddress. a NetAddress takes 2 parameters,
   * an ip address and a port number. myRemoteLocation is used as parameter in
   * oscP5.send() when sending osc packets to another computer, device, 
   * application. usage see below. for testing purposes the listening port
   * and the port of the remote location address are the same, hence you will
   * send messages back to this sketch.
   */
  myRemoteLocation = new NetAddress("127.0.0.1",8001);
}

void draw() {
  background(0);
  noStroke();
}

void oscEvent(OscMessage theOscMessage) {
  /* check if theOscMessage has the address pattern we are looking for. */
  
  if(theOscMessage.checkAddrPattern("/Rumbo")==true) {
    int firstValue = theOscMessage.get(0).intValue(); 
    if (firstValue == -1){
      fill(227, 55, 24);
      ellipse (50, 100, 30, 30);
    }
    if (firstValue == 1){
      fill(139, 227, 39);
      ellipse (150, 100, 30, 30);
    }
  } 
  println("### received an osc message. with address pattern "+theOscMessage.addrPattern());
}
