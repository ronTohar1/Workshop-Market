import Restriction from "./Restriction";

class AfterHour extends Restriction {
    hour: number;
    constructor(hour: number) {
        super();
        this.hour = hour;
    }
  }
  
  export default AfterHour