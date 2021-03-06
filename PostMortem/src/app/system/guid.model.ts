
    export class Guid {
        constructor( public guid1: string ) {
            this.guid = guid1;
        }

        private guid: string;
        // Static member
        static MakeNew(): Guid {
            let result: string;
            let i: string;
            let j: number;

            result = '';
            for (j = 0; j < 32; j++) {
                if (j === 8 || j === 12 || j === 16 || j === 20) {
                    result = result + '-';
                }
                i = Math.floor(Math.random() * 16).toString(16).toUpperCase();
                result = result + i;
            }
            return new Guid(result);
        }
        public ToString(): string {
            return this.guid;
        }
    }
