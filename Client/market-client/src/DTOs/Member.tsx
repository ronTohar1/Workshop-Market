
class Member {
    id: number;
    userName: string;
    loggedIn: boolean;
    
  
    constructor(
      id: number,
      name: string,
      loggedIn: boolean
  
    ) {
      this.id = id;
      this.userName = name;
      this.loggedIn = loggedIn;
    }
  }
  
  export default Member;
  
