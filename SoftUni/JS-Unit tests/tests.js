let Console = require('./console');
let assert = require('chai').assert;
describe("Tests of Console", function() {
	it('Should return correct value with only one argument', function (){
		let actualOutput = Console.writeLine('Pesho');
		let expectedOutput = 'Pesho';

		assert.deepEqual(actualOutput, expectedOutput);
	});
});
