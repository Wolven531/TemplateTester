import * as React from 'react'

import { Upgrades } from '../../models/Upgrades'

import './UpgradesDisplay.css'

const UpgradesDisplay = ({ numResource }) => (
	<section className="upgrades-display">
		<h2>Upgrades:</h2>
		{Object.getOwnPropertyNames(Upgrades)
			.map(upgradeKey => {
				const upgrade = Upgrades[upgradeKey]
				const { cost, name } = upgrade
				const percentage = numResource >= cost ? 100 : (1.0 * numResource / cost * 100).toFixed(2)

				return (
					<article className="upgrade-container" key={name.replace(' ', '-')}>
						<h3>{name}</h3>
						<p>
						<span className="cost">{cost} resources ({percentage}%)</span>
							<progress value={percentage} max={100}>
								{percentage}%
							</progress>
						</p>
					</article>
				)
			}
		)}
	</section>
)

export { UpgradesDisplay }
