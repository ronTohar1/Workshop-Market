
class Member {
    id: number;
    name: string;
    loggedIn: boolean;
    
  
    constructor(
      id: number,
      name: string,
      loggedIn: boolean
  
    ) {
      this.id = id;
      this.name = name;
      this.loggedIn = loggedIn;
    }
  }
  
  export default Member;
  