let Parser = require("./examSol");
let assert = require("chai").assert;
describe("MyTests", () => {
    describe('Constructor tests', function () {
        it ('Instantiation tests', function () {
            let parser = new Parser('[ {"Nancy":"architect"},{"John":"developer"},{"Kate": "HR"} ]');
            let expectedOutput = [ {"Nancy":"architect"},{"John":"developer"},{"Kate": "HR"} ];
            let actualOutput = parser._data;

            assert.deepEqual(expectedOutput, actualOutput);
            assert.deepEqual(parser._log, []);
            assert.deepEqual(parser._addToLog(), 'Added to log');
        });
    });
    describe('Getter tests', function() {
        it ('should return correct value', function () {
            let parser = new Parser('[ {"Nancy":"architect"},{"John":"developer"},{"Kate": "HR"} ]');
            let expectedOutput = [ { Nancy: 'architect' },
            { John: 'developer' },
            { Kate: 'HR' } ];

            let actualOutput = parser.data;
            assert.deepEqual(expectedOutput, actualOutput);
        });
    });
    describe('print tests', function () {
        it ('Should print correctly', function () {
            let parser = new Parser('[ {"Nancy":"architect"},{"John":"developer"},{"Kate": "HR"} ]');
            parser.addEntries("Steven:tech-support Edd:administrator");
            parser.removeEntry("Kate");
            let actualOutput = parser.print();
            let expectedOutput = 'id|name|position\n0|Nancy|architect\n1|John|developer\n2|Steven|tech-support\n3|Edd|administrator';

            let parserLogs = parser._log[2];
            let expectedLog = '2: print';

            assert.deepEqual(actualOutput, expectedOutput);
            assert.deepEqual(parserLogs, expectedLog);
        }) 
    })

    describe('removeEntries tests', function() {
        it ('should remove the entry correctly', function() {
            let parser = new Parser('[ {"Nancy":"architect"},{"John":"developer"},{"Kate": "HR"} ]');
            let actualOutput = parser.removeEntry('Kate');
            let expectedOutput = "Removed correctly!";

            assert.deepEqual(actualOutput, expectedOutput);
        });
    });

    describe('addEntries tests', function() {
        it ('should add the entry correctly', function() {
            let parser = new Parser('[]');

            let actualOutput = parser.addEntries("Steven:tech-support");
            
            let expectedOutput = "Entries added!";

            let expectedLength = 1;
            let actualLenght = parser._data.length

            assert.deepEqual(actualOutput, expectedOutput);
            assert.deepEqual(expectedLength, actualLenght);
        });
    });
});