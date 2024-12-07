Back: [[___READ ME - Project Documentation___]]
Here is a Detection Area:
![[Pasted image 20241207025201.png]]
When the [[THREAT]] enters it, it will look like this:
![[Pasted image 20241207025213.png]]

The behavior of this object is fairly simple. It consists of a simple script (DetectionArea.CS), which, when an object with a collider and the "Thread" tag enters, it will do the following:
 - Change the material of the area (ie: turn it red)
 - Output to the [[EVENTS CONSOLE]] that the device has been detected.
When entered by [[CIVILIANS]], they will display this to the [[EVENTS CONSOLE]] as well.