import { Fandom } from "./Fandom";
import { Fanfic } from "./Fanfic";

export interface FanficRatingFandomTemplate{
   fanfic:Fanfic,
   rating:number,
   fandom:Fandom
}
