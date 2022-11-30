import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    index1 : string = 'Dogs are great – they provide us with love,companionship and are always there when we need them.But did you know there’s far more to dogs than meets the eye? ';
    index2 : string = ' The area of cells in the brain that detect different smells is around 40 timeslarger in dogs than humans. This means that your dog can pick up on way more smellsthan we ever could. This is why dogs are often used to sniff out people,drugs and even money!';
    index3 : string = 'Yup,medical detection dogs are a thing. Because their sense of smell is so great, some dogs can be trained to sniff out medical conditions. They are used to diagnose a particular condition or to alert their owners if they need more medication. Some are even being trained to sniff out Covid-19!One of these incredible dogs is Medical Detection Dog Pal (pictured above), who was awarded the PDSA Order of Merit. Pal played a vital role in diabetic owner Claire’s life by alerting her of changes in her blood sugar.';
    index4 :string = 'Dogs have 18 muscles to move their ears.Theyre good for more than just taking scritches. These 18 muscles allow dogs to move their ears in intricate ways, which is important to picking up sounds. Plus, your dogs ears can give you a clue as to how theyre feeling: If theyre laying flat back against their head, your pooch may be scared. If theyre pointing straight up, theyre alert and on the case. ';
    index5 :string = 'A dogs nose is its fingerprint. Dog noses have unique patterns that can serve to identify them, similar to human fingerprints. And in April 2021, dog food company IAMS launched an app that uses this fact to help reunite lost pups with their owners. After you download NOSEiD, scan your pets nose and upload it to their database. If they ever escape your yard, any concerned human who finds them can search the app for a match.';
    index6 :string = 'Dogs may be able to fall in love with you. Thanks to science, you can know, for a fact, that your dog loves you back. A 2015 study at Azabu University in Japan found an uptick in the level of oxytocin (sometimes known as "the love hormone") in both dogs and their owners when they stare at each other.';
    index7 :string = 'Dogs sweat through their paws only. Dogs have sweat glands only on their paws, not the rest of their bodies. That little surface area isnt enough to cool them down, however, which is why they ventilate and exchange heat through panting. ';
    index8 :string = 'Small dogs can hear sounds in higher ranges than big dogs. In addition to being able to move in various ways, dogs ears are also able to detect much higher frequency sounds than human ears. In fact, dogs can hear sounds that are two times beyond our range—and it appears small dogs are actually better at it. According to Science Focus, thats because the smaller a mammals head is, the higher frequencies it can pick up and compare in each ear. Thats how they (and we) figure out where sounds are coming from.';
    index9 :string = 'Dogs mark their territory with glands in their paws. Dogs are not, in fact, trying to clumsily bury their poop when they scratch the ground after they go. They are just performing yet another territory-marking ritual. With the glands on their paws, they spread their scent and let other dogs know they are around. ';
    index10 :string = 'Dogs are more aggressive when being walked by a man.The presence of a leash, the sex of the owner, and the sex of the dog all play a part in the aggressiveness of a dog when theyre being walked. Dogs being walked by men are four times more likely to attack and bite another dog. Why? Because dogs not only react to the behavior and posture of other dogs around them but also to people. ';
  items: string[] ;

    constructor() {

    }

  ngOnInit(): void {
    // this.items.push(this.index2 , this.index3 , this.index4 , this.index5 , this.index6 , this.index7 , this.index8 ,this.index9 )
    // console.log(this.items.values())
  }

}
