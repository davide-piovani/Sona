using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrittenPart : MonoBehaviour {

    private int i = 0;
    private int j = 0;

    private String [] names = {"Charlie", "Charlie", "Jack", "Hannah", "Jack", "Charlie", "Jack", "Charlie", "Hannah"};
    private String [] lines = {"How much left?", "Great.\nNow what?", "I think the thing on our left was the main circuitry",
     "Do you think you can repair it?", "Maybe. We need to find the component and the general switch, thought", "Where?", "Probably in the other rooms",
     "Yeah, slight problem: we can't see", "Wait, I found this"};

    private String [] titles = {"To doctor Mark Santos", "Report #382 (SONA Development Team)", "Report #383 (SONA Development Team)"};
    private String [] contents = {"This is doctor James Dertham and I am contacting you for a very urgent matter. I am deeply against the anticipation of the test of SONA, of which I was informed not earlier than this morning. I understand that the council wants some result on the experiment, but, despite the calculation showing an expected 99% success rate, we are still too early for a full scale practical test. I implore you, as an important member of the council to reconsider the decision taken and allow my team more time to conduct our research and avoid said 1% possibility of failure. Heaven knows what could go wrong if we are mistaken\nSigned\nDoctor James Dertham",
        "I am here today to report what could likely be the worst disaster in modern history. As ordered, today has been conducted the final test on SONA, a practical employment of the experimental technology. The result is the almost complete obliteration of the lifeforms in a three kilometers radius. A rescue team has been dispatched to the nearby city to look for survivors. Of the development team, there are only two survivors: myself and my assistant and son Jack. I was in the observation room, which was protected from the explosion and he was in the control room which, while not as protected, did its job in shielding him from the worst of the explosion. All the survivors are being sent to the nearest functioning hospital for a complete checkup.\nRegards\nDoctor James Dertham",
        "Contrary to my previous expectations, the previous report is not the last one. The rescue team has recovered two more survivors in the city. According to them, the two were in the caveau of the city bank at the moment of the explosion, which has acted as a shield for them.\nI have been completely cleared, but, according to the doctors, the others have shown some abnormal behavior in their vitals. Given that the consequences of the exposition to the explosion are unknown, it has been decided to isolate them and I have been tasked with their observation. An abandoned petrol platform is being equipped with the necessary machinery and we are to depart as soon as possible.\nSigned\nDoctor James Dertham"};

    public String GetName() {
        if(i<names.Length){
            //print("WRITTEN PART: (name) index " + i);
            return (names[i]);
        } else {
            return null;
        }
    }
    
    public String GetDialContent () {
        if(i<lines.Length){
            String line = lines[i];
            i++;
            return (line);
        } else {
            return null;
        }
    }

    public String GetTitle () {
        if(j<titles.Length){
            return (titles[j]);
        } else {
            return null;
        }

    }

    public String GetDocContent () {
        String lines;
        if(j<contents.Length){
            lines = contents[j];
            j++;
            return (lines);
        } else {
            return null;
        }
    }
}
